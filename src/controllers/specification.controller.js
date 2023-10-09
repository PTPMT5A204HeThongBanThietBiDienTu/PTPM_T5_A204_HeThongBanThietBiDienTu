const { Specification, Product } = require("../models/index")

const create = async (req, res, next) => {
    const product = await Product.findByPk(req.body.proId)
    if (!product) {
        return res.status(400).json({
            success: false,
            message: 'Invalid proId'
        })
    }

    const specification = await Specification.create(req.body)
    const newSpecification = await Specification.findByPk(specification.id, {
        include: [{
            model: Product,
            as: 'product',
            attributes: ['id', 'name']
        }]
    })

    return res.status(201).json({
        success: true,
        data: newSpecification
    })
}

const getAllByProId = async (req, res, next) => {
    const product = await Product.findByPk(req.params.id)
    if (!product) {
        return res.status(400).json({
            success: false,
            message: 'Invalid proId'
        })
    }

    const specifications = await Specification.findAll({
        where: { proId: req.params.id },
        include: [{
            model: Product,
            as: 'product',
            attributes: ['id', 'name']
        }]
    })

    return res.status(200).json({
        success: true,
        data: specifications
    })
}

module.exports = {
    create,
    getAllByProId
}