const { Category } = require("../models/index")

const getAll = async (req, res, next) => {
    const categories = await Category.findAll()

    return res.status(200).json({
        success: true,
        data: categories
    })
}

const getById = async (req, res, next) => {
    const category = await Category.findByPk(req.params.id)
    if (!category) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    return res.status(200).json({
        success: true,
        data: category
    })
}

const create = async (req, res, next) => {
    const category = await Category.create(req.body)

    return res.status(201).json({
        success: true,
        data: category
    })
}

const update = async (req, res, next) => {
    const category = await Category.findByPk(req.params.id)
    if (!category) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    const result = Category.update(req.body, { where: { id: req.params.id } })
    return getById(req, res, next)
}

const remove = async (req, res, next) => {
    const category = await Category.findByPk(req.params.id)
    if (!category) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    const result = Category.destroy({ where: { id: req.params.id } })
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