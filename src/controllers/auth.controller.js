const bcrypt = require("bcrypt")
const { User, Role } = require("../models/index")
const JWTService = require("../services/jwt.service")

const register = async (req, res, next) => {
    const role = await Role.findOne({ where: { name: 'user' } })
    req.body.roleId = role.id

    const user = await User.create(req.body)

    return res.status(201).json({
        message: 'Register success'
    })
}

const login = async (req, res, next) => {
    const user = await User.findOne({
        where: { email: req.body.email },
        include: [{
            model: Role,
            as: 'role',
            attributes: ['id', 'name']
        }]
    })

    if (!user) {
        return res.status(404).json({
            success: false,
            message: 'Email not found'
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
        message: 'Logout success'
    })
}

const refreshToken = async (req, res, next) => {
    let token = req.session.userToken?.refreshToken
    if (!token) {
        return res.status(404).jsonn({
            success: false,
            message: 'refresh token not found'
        })
    }

    const { id } = JWTService.decodeRefreshToken(token)
    const user = await User.findByPk(id, {
        include: [{
            model: Role,
            as: 'role',
            attributes: ['id', 'name']
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
        message: 'Refresh token success'
    })
}

const getInfo = async (req, res, next) => {
    const { data } = JWTService.decodeAccessToken(req.session.userToken.accessToken)
    const user = await User.findByPk(data.id, {
        include: [{
            model: Role,
            as: 'role',
            attributes: ['id', 'name']
        }],
        attributes: ['id', 'name', 'email', 'address', 'phone', 'roleId']
    })

    return res.status(200).json({
        success: true,
        data: user
    })
}

module.exports = {
    register,
    login,
    logout,
    refreshToken,
    getInfo
}