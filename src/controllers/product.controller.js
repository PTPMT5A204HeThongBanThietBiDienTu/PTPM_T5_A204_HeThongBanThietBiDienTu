const fs = require("fs")
const { Op } = require('sequelize')
const { Product, Category, Brand, Image, Recommend } = require("../models/index")
const PAGE_SIZE = 8

const getAll = async (req, res, next) => {
    let page = req.query.page
    let products = []
    const totalCount = await Product.count()
    const totalPages = Math.ceil(totalCount / PAGE_SIZE)

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
        data: products,
        totalPages: totalPages
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
    const totalCount = await Product.count({
        where: { catId: req.params.id }
    })
    const totalPages = Math.ceil(totalCount / PAGE_SIZE)

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
        data: products,
        totalPages: totalPages
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
    const totalCount = await Product.count({
        where: { braId: req.params.id }
    })
    const totalPages = Math.ceil(totalCount / PAGE_SIZE)

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
        data: products,
        totalPages: totalPages
    })
}

const getAllByPrice = async (req, res, next) => {
    let page = req.query.page
    let products = []
    const category = await Category.findByPk(req.query.catId)
    if (!category) {
        return res.status(400).json({
            success: false,
            message: 'Invalid catId'
        })
    }

    if (req.query.braId) {
        const brand = await Brand.findByPk(req.query.braId)
        if (!brand) {
            return res.status(400).json({
                success: false,
                message: 'Invalid braId'
            })
        }

        const totalCount = await Product.count({
            where: {
                catId: category.id,
                braId: brand.id,
                price: {
                    [Op.gt]: req.body.minPrice,
                    [Op.lt]: req.body.maxPrice
                }
            }
        })
        const totalPages = Math.ceil(totalCount / PAGE_SIZE)


        if (page) {
            page = parseInt(page)
            products = await Product.findAll({
                where: {
                    catId: category.id,
                    braId: brand.id,
                    price: {
                        [Op.gt]: req.body.minPrice,
                        [Op.lt]: req.body.maxPrice
                    }
                },
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
                where: {
                    catId: category.id,
                    braId: brand.id,
                    price: {
                        [Op.gt]: req.body.minPrice,
                        [Op.lt]: req.body.maxPrice
                    }
                },
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
            data: products,
            totalPages: totalPages
        })
    }
    else {
        const totalCount = await Product.count({
            where: {
                catId: category.id,
                price: {
                    [Op.gt]: req.body.minPrice,
                    [Op.lt]: req.body.maxPrice
                }
            }
        })
        const totalPages = Math.ceil(totalCount / PAGE_SIZE)


        if (page) {
            page = parseInt(page)
            products = await Product.findAll({
                where: {
                    catId: category.id,
                    price: {
                        [Op.gt]: req.body.minPrice,
                        [Op.lt]: req.body.maxPrice
                    }
                },
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
                where: {
                    catId: category.id,
                    price: {
                        [Op.gt]: req.body.minPrice,
                        [Op.lt]: req.body.maxPrice
                    }
                },
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
            data: products,
            totalPages: totalPages
        })
    }
}

const getAllByCatIdAndBraId = async (req, res, next) => {
    const category = await Category.findByPk(req.query.catId)
    if (!category) {
        return res.status(400).json({
            success: false,
            message: 'Invalid catId'
        })
    }

    const brand = await Brand.findByPk(req.query.braId)
    if (!brand) {
        return res.status(400).json({
            success: false,
            message: 'Invalid braId'
        })
    }

    let page = req.query.page
    let products = []
    const totalCount = await Product.count({
        where: {
            catId: req.query.catId,
            braId: req.query.braId
        }
    })
    const totalPages = Math.ceil(totalCount / PAGE_SIZE)

    if (page) {
        page = parseInt(page)
        products = await Product.findAll({
            where: {
                catId: req.query.catId,
                braId: req.query.braId
            },
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
            where: {
                catId: req.query.catId,
                braId: req.query.braId
            },
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
        data: products,
        totalPages: totalPages
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
    const content = `%${req.body.content}%`
    let page = req.query.page
    let products = []
    let totalCount = 0;
    let totalPages = 0;

    const whereConditions = {
        [Op.or]: [
            {
                name: {
                    [Op.like]: content
                }
            },
            {
                '$category.name$': {
                    [Op.like]: content
                }
            },
            {
                '$brand.name$': {
                    [Op.like]: content
                }
            }
        ]
    }

    const whereConditionsPrice = {
        [Op.or]: [
            {
                name: {
                    [Op.like]: content
                }
            },
            {
                '$category.name$': {
                    [Op.like]: content
                }
            },
            {
                '$brand.name$': {
                    [Op.like]: content
                }
            }
        ],
        price: {
            [Op.gt]: req.body.minPrice,
            [Op.lt]: req.body.maxPrice
        }
    }

    if (req.body.content && req.body.minPrice && req.body.maxPrice) {
        totalCount = await Product.count({
            include: [
                {
                    model: Category,
                    as: 'category'
                },
                {
                    model: Brand,
                    as: 'brand'
                }
            ],
            where: whereConditionsPrice
        })
        totalPages = Math.ceil(totalCount / PAGE_SIZE)

        if (page) {
            page = parseInt(page)
            products = await Product.findAll({
                include: [
                    {
                        model: Category,
                        as: 'category'
                    },
                    {
                        model: Brand,
                        as: 'brand'
                    }
                ],
                where: whereConditionsPrice,
                offset: (page - 1) * PAGE_SIZE,
                limit: PAGE_SIZE
            })
        }
        else {
            products = await Product.findAll({
                include: [
                    {
                        model: Category,
                        as: 'category'
                    },
                    {
                        model: Brand,
                        as: 'brand'
                    }
                ],
                where: whereConditionsPrice
            })
        }
    } else {
        totalCount = await Product.count({
            include: [
                {
                    model: Category,
                    as: 'category'
                },
                {
                    model: Brand,
                    as: 'brand'
                }
            ],
            where: whereConditions
        })
        totalPages = Math.ceil(totalCount / PAGE_SIZE)

        if (page) {
            page = parseInt(page)
            products = await Product.findAll({
                include: [
                    {
                        model: Category,
                        as: 'category'
                    },
                    {
                        model: Brand,
                        as: 'brand'
                    }
                ],
                where: whereConditions,
                offset: (page - 1) * PAGE_SIZE,
                limit: PAGE_SIZE
            })
        }
        else {
            products = await Product.findAll({
                include: [
                    {
                        model: Category,
                        as: 'category'
                    },
                    {
                        model: Brand,
                        as: 'brand'
                    }
                ],
                where: whereConditions
            })
        }
    }

    return res.status(200).json({
        success: true,
        data: products,
        totalPages: totalPages,
        totalCount: totalCount
    })
}

const getAllAccompany = async (req, res, next) => {
    const product = await Product.findByPk(req.params.id)

    if (!product) {
        return res.status(400).json({
            success: false,
            message: 'Invalid ID'
        })
    }


    const accompanys = await Recommend.findAll({
        where: { proId: req.params.id }
    })

    return res.status(200).json({
        success: true,
        data: accompanys
    })
}

module.exports = {
    getAll,
    getAllByCatId,
    getAllByBraId,
    getAllByPrice,
    getAllByCatIdAndBraId,
    getById,
    getAllAccompany,
    create,
    update,
    remove,
    search
}