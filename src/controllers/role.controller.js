const { Role } = require("../models/index")

const getAll = async (req, res, next) => {
    const roles = await Role.findAll()

    return res.status(200).json({
        success: true,
        data: roles
    })
}

const getById = async (req, res, next) => {
    const role = await Role.findByPk(req.params.id)
    if (!role) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    return res.status(200).json({
        success: true,
        data: role
    })
}

const create = async (req, res, next) => {
    Reflect.deleteProperty(req.body, 'id')
    const role = await Role.create(req.body)

    return res.status(201).json({
        success: true,
        data: role
    })
}

const update = async (req, res, next) => {
    const role = await Role.findByPk(req.params.id)
    if (!role) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    const result = await Role.update(req.body, { where: { id: req.params.id } })
    return getById(req, res, next)
}

const remove = async (req, res, next) => {
    const role = await Role.findByPk(req.params.id)
    if (!role) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    const result = await Role.destroy({ where: { id: req.params.id } })
    return res.status(200).json({
        success: true
    })
}

module.exports = {
    getAll,
    getById,
    create,
    update,
    remove
}