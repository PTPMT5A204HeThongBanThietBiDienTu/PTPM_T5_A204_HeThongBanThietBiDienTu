const fs = require("fs")
const { QueryTypes } = require('sequelize')
const { Product, Category, Brand, Image } = require("../models/index")
const PAGE_SIZE = 4

const getAll = async (req, res, next) => {
    let page = req.query.page
    let products = []

    if (page) {
        page = parseInt(page)
        products = await Product.findAll({
            include: [
                {
                    model: Category,
                    as: 'category',
                    attributes: ['id', 'name']
                },
                {
                    model: Brand,
                    as: 'brand',
                    attributes: ['id', 'name']
                }
            ],
            offset: (page - 1) * PAGE_SIZE,
            limit: PAGE_SIZE
        })
    }
    else {
        products = await Product.findAll({
            include: [
                {
                    model: Category,
                    as: 'category',
                    attributes: ['id', 'name']
                },
                {
                    model: Brand,
                    as: 'brand',
                    attributes: ['id', 'name']
                }
            ]
        })
    }

    return res.status(200).json({
        success: true,
        data: products
    })
}

const getAllByCatId = async (req, res, next) => {
    const category = await Category.findByPk(req.params.id)
    if (!category) {
        return res.status(400).json({
            success: false,
            message: 'Invalid catId'
        })
    }

    let page = req.query.page
    let products = []

    if (page) {
        page = parseInt(page)
        products = await Product.findAll({
            where: { catId: req.params.id },
            include: [
                {
                    model: Category,
                    as: 'category',
                    attributes: ['id', 'name']
                },
                {
                    model: Brand,
                    as: 'brand',
                    attributes: ['id', 'name']
                }
            ],
            offset: (page - 1) * PAGE_SIZE,
            limit: PAGE_SIZE
        })
    }
    else {
        products = await Product.findAll({
            where: { catId: req.params.id },
            include: [
                {
                    model: Category,
                    as: 'category',
                    attributes: ['id', 'name']
                },
                {
                    model: Brand,
                    as: 'brand',
                    attributes: ['id', 'name']
                }
            ]
        })
    }

    return res.status(200).json({
        success: true,
        data: products
    })
}

const getAllByBraId = async (req, res, next) => {
    const brand = await Brand.findByPk(req.params.id)
    if (!brand) {
        return res.status(400).json({
            success: false,
            message: 'Invalid braId'
        })
    }

    let page = req.query.page
    let products = []

    if (page) {
        page = parseInt(page)
        products = await Product.findAll({
            where: { braId: req.params.id },
            include: [
                {
                    model: Category,
                    as: 'category',
                    attributes: ['id', 'name']
                },
                {
                    model: Brand,
                    as: 'brand',
                    attributes: ['id', 'name']
                }
            ],
            offset: (page - 1) * PAGE_SIZE,
            limit: PAGE_SIZE
        })
    }
    else {
        products = await Product.findAll({
            where: { braId: req.params.id },
            include: [
                {
                    model: Category,
                    as: 'category',
                    attributes: ['id', 'name']
                },
                {
                    model: Brand,
                    as: 'brand',
                    attributes: ['id', 'name']
                }
            ]
        })
    }

    return res.status(200).json({
        success: true,
        data: products
    })
}

const getById = async (req, res, next) => {
    const product = await Product.findByPk(req.params.id, {
        include: [
            {
                model: Category,
                as: 'category',
                attributes: ['id', 'name']
            },
            {
                model: Brand,
                as: 'brand',
                attributes: ['id', 'name']
            }
        ]
    })

    if (!product) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }


    const images = await Image.findAll({
        where: { proId: req.params.id },
        attributes: ['id', 'imgSrc']
    })

    return res.status(200).json({
        success: true,
        data: product,
        images: images
    })
}

const create = async (req, res, next) => {
    let result
    if (req.body.catId) {
        const category = await Category.findByPk(req.body.catId)
        if (!category)
            result = 'Invalid catId'
    }

    if (req.body.braId) {
        const brand = await Brand.findByPk(req.body.braId)
        if (!brand)
            result = 'Invalid braId'
    }

    if (result) {
        fs.unlinkSync(req.files[0].path)
        return res.status(400).json({
            success: false,
            message: result
        })
    }

    req.body.img = `/images/${req.files[0].filename}`
    try {
        const product = await Product.create(req.body)
        return res.status(201).json({
            success: true,
            data: product
        })
    } catch (error) {
        fs.unlinkSync(req.files[0].path)
        next(error)
    }
}

const update = async (req, res, next) => {
    const product = await Product.findByPk(req.params.id)
    if (!product) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    let error
    if (req.body.catId) {
        const category = await Category.findByPk(req.body.catId)
        if (!category)
            error = 'Invalid catId'
    }

    if (req.body.braId) {
        const brand = await Brand.findByPk(req.body.braId)
        if (!brand)
            error = 'Invalid braId'
    }

    if (error) {
        return res.status(400).json({
            success: false,
            message: result
        })
    }

    const result = await Product.update(req.body, { where: { id: req.params.id } })
    return getById(req, res, next)
}

const remove = async (req, res, next) => {
    const product = await Product.findByPk(req.params.id)
    if (!product) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }

    const path = './public' + product.img
    const result = await Product.destroy({ where: { id: req.params.id } })

    if (result === 1)
        fs.unlinkSync(path)

    return res.status(200).json({
        success: true
    })
}

const search = async (req, res, next) => {
    let queryStr = `select p.id, p.name, p.price, p.quantity, p.description, p.img, p.catId, p.braId 
                    from Products p 
                    left join Categories c on p.catId = c.id
                    left join Brands b on p.braId = b.id
                    where p.name like :content or c.name like :content or b.name like :content`
    const products = await Product.sequelize.query(queryStr, {
        replacements: { content: '%' + req.body.content + '%' },
        type: QueryTypes.SELECT
    })

    return res.status(200).json({
        success: true,
        data: products
    })
}

module.exports = {
    getAll,
    getAllByCatId,
    getAllByBraId,
    getById,
    create,
    update,
    remove,
    search
}