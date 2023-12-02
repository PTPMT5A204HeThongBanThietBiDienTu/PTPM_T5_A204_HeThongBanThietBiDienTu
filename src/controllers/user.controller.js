const { User, Role } = require("../models/index")

const getAll = async (req, res, next) => {
    const users = await User.findAll({
        include: [{
            model: Role,
            as: 'role',
            attributes: ['id', 'roleName']
        }]
    })

    return res.status(200).json({
        success: true,
        data: users
    })
}

const create = async (req, res, next) => {
    if (req.body.roleId) {
        const role = await Role.findByPk(req.body.roleId)
        if (!role) {
            return res.status(400).json({
                success: false,
                message: 'Invalid roleId'
            })
        }
    }

    const user = await User.create(req.body)
    const userNew = await User.findByPk(user.id, {
        include: [{
            model: Role,
            as: 'role',
            attributes: ['id', 'roleName']
        }]
    })
    return res.status(201).json({
        success: true,
        data: userNew
    })
}

module.exports = {
    getAll,
    create
}