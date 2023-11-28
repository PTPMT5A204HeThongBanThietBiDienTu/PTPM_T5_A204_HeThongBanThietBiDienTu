const { Brand, Category_Brand, Category } = require('../models/index')

const getAll = async (req, res, next) => {
    const brands = await Brand.findAll()
    return res.status(200).json({
        success: true,
        data: brands
    })
}

const getAllByCatId = async (req, res, next) => {
    const category = await Category.findByPk(req.params.id)
    if (!category) {
        return res.status(400).json({
            success: false,
            message: 'Invalid catID'
        })
    }

    const brands = await Category_Brand.findAll({
        where: { catId: req.params.id },
        include: {
            model: Brand,
            as: 'brand',
            attributes: ['name']
        }
    })

    return res.status(200).json({
        success: true,
        data: brands
    })
}

const getById = async (req, res, next) => {
    const brand = await Brand.findByPk(req.params.id)
    if (!brand) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    return res.status(200).json({
        success: true,
        data: brand
    })
}

const create = async (req, res, next) => {
    const brand = await Brand.create(req.body)

    return res.status(201).json({
        success: true,
        data: brand
    })
}

const update = async (req, res, next) => {
    const brand = await Brand.findByPk(req.params.id)
    if (!brand) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    const result = await Brand.update(req.body, { where: { id: req.params.id } })
    return getById(req, res, next)
}

const remove = async (req, res, next) => {
    const brand = await Brand.findByPk(req.params.id)
    if (!brand) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    const result = await Brand.destroy({ where: { id: req.params.id } })
    return res.status(200).json({
        success: true
    })
}

module.exports = {
    getAll,
    getAllByCatId,
    getById,
    create,
    update,
    remove
}