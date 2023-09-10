import { Role } from "../models/index"

const getAll = (req, res, next) => {
    Role.findAll()
        .then(roles => res.status(200).json({
            success: true,
            data: roles
        }))
        .catch(err => next(err))
}

const getById = (req, res, next) => {
    Role.findByPk(req.params.id)
        .then(role => {
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
        })
        .catch(err => next(err))
}

const create = (req, res, next) => {
    Role.create(req.body)
        .then(role => res.status(201).json({
            success: true,
            data: role
        }))
        .catch(err => next(err))
}

const update = async (req, res, next) => {
    const role = await Role.findByPk(req.params.id)
    if (!role) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    Role.update(req.body, { where: { id: req.params.id } })
        .then(() => getById(req, res, next))
        .catch(err => next(err))
}

const remove = async (req, res, next) => {
    const role = await Role.findByPk(req.params.id)
    if (!role) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    Role.destroy({ where: { id: req.params.id } })
        .then(() => res.status(200).json({
            success: true
        }))
        .catch(err => next(err))
}

module.exports = {
    getAll,
    getById,
    create,
    update,
    remove
}