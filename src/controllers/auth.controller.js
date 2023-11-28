const bcrypt = require("bcrypt")
const { User, Role } = require("../models/index")
const JWTService = require("../services/jwt.service")
const jwtService = require("../services/jwt.service")

const register = async (req, res, next) => {
    const role = await Role.findOne({ where: { roleName: 'user' } })
    req.body.roleId = role.id

    const user = await User.create(req.body)

    return res.status(201).json({
        success: true,
        message: 'Register success'
    })
}

const login = async (req, res, next) => {
    const user = await User.findOne({
        where: { email: req.body.email },
        include: [{
            model: Role,
            as: 'role',
            attributes: ['id', 'roleName']
        }]
    })

    if (!user) {
        return res.status(404).json({
            success: false,
            message: 'Email not found'
        })
    }

    if (!user.is_Active) {
        return res.status(404).json({
            success: false,
            message: 'Your account is locked'
        })
    }

    const validPW = bcrypt.compareSync(req.body.password, user.password)
    if (!validPW) {
        return res.status(400).json({
            success: false,
            message: 'Incorrect password'
        })
    }

    const userToken = {
        accessToken: JWTService.generateAccessToken({
            id: user.id,
            role: user.role.name
        }),
        refreshToken: JWTService.generateRefreshToken(user.id)
    }

    req.session.userToken = userToken

    return res.status(200).json({
        success: true,
        message: 'Login success'
    })
}

const logout = async (req, res, next) => {
    await req.session.destroy(err => {
        if (err) {
            console.log(err)
        }
    })

    res.clearCookie('CellphoneS_API', { path: '/' })

    return res.status(200).json({
        success: true,
        message: 'Logout success'
    })
}

const refreshToken = async (req, res, next) => {
    let token = req.session.userToken?.refreshToken
    if (!token) {
        return res.status(404).json({
            success: false,
            message: 'refresh token not found'
        })
    }

    const { id } = JWTService.decodeRefreshToken(token)
    const user = await User.findByPk(id, {
        include: [{
            model: Role,
            as: 'role',
            attributes: ['id', 'roleName']
        }]
    })

    if (!user) {
        return res.status(404).json({
            success: false,
            message: 'user not found'
        })
    }

    const userToken = {
        accessToken: JWTService.generateAccessToken({
            id: user.id,
            role: user.role.name
        }),
        refreshToken: JWTService.generateRefreshToken(user.id)
    }

    req.session.userToken = userToken

    return res.status(200).json({
        success: true,
        message: 'Refresh token success'
    })
}

const getInfo = async (req, res, next) => {
    const { data } = JWTService.decodeAccessToken(req.session.userToken.accessToken)
    const user = await User.findByPk(data.id, {
        include: [{
            model: Role,
            as: 'role',
            attributes: ['id', 'roleName']
        }],
        attributes: ['id', 'name', 'email', 'address', 'phone', 'roleId']
    })

    return res.status(200).json({
        success: true,
        data: user
    })
}

const updateInfo = async (req, res, next) => {
    const { data } = JWTService.decodeAccessToken(req.session.userToken.accessToken)

    await User.update(req.body, { where: { id: data.id } })
    getInfo(req, res, next)
}

const changePass = async (req, res, next) => {
    const { data } = jwtService.decodeAccessToken(req.session.userToken.accessToken)
    const user = await User.findByPk(data.id)

    const validPW = bcrypt.compareSync(req.body.passOld, user.password)
    if (!validPW) {
        return res.status(400).json({
            success: false,
            message: 'Incorrect old password'
        })
    }

    await user.update({ password: req.body.passNew }, { where: { id: data.id } })
    return res.status(200).json({
        success: true,
        message: 'Change password success !'
    })
}

module.exports = {
    register,
    login,
    logout,
    refreshToken,
    getInfo,
    updateInfo,
    changePass
}