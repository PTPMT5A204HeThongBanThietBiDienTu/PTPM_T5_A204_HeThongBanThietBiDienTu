const { Cart, Product, User } = require("../models/index")
const JWTService = require("../services/jwt.service")

const getById = async (req, res, next) => {
    const cart = await Cart.findByPk(req.params.id, {
        include: [{
            model: Product,
            as: 'product',
            attributes: ['id', 'name', 'price', 'img']
        }]
    })

    if (!cart) {
        return res.status(400).json({
            success: false,
            data: 'Invalid cartId'
        })
    }

    return res.status(200).json({
        success: true,
        data: cart
    })
}

const getAllByUserId = async (req, res, next) => {
    const { data } = JWTService.decodeAccessToken(req.session.userToken.accessToken)

    console.log(data)
    const cart = await Cart.findAll({
        where: { userId: data.id },
        include: [{
            model: Product,
            as: 'product',
            attributes: ['id', 'name', 'price', 'img']
        }]
    })

    return res.status(200).json({
        success: true,
        data: cart
    })
}

const create = async (req, res, next) => {
    const { data } = JWTService.decodeAccessToken(req.session.userToken.accessToken)
    const user = await User.findByPk(data.id)
    if (!user) {
        return res.status(404).json({
            success: false,
            message: 'User not found.'
        })
    }

    const product = await Product.findByPk(req.body.proId)
    if (!product) {
        return res.status(400).json({
            success: false,
            message: 'Invalid proId'
        })
    }

    const proInCart = await Cart.findOne({
        where: {
            proId: product.id,
            userId: user.id
        }
    })

    if (!proInCart) {
        const cart = await Cart.create({
            userId: user.id,
            proId: req.body.proId
        })
    }
    else {
        await Cart.update(
            { quantity: proInCart.quantity + 1 },
            {
                where: {
                    id: proInCart.id,
                    userId: user.id
                }
            }
        )
    }

    const newCart = await Cart.findOne({
        where: { proId: product.id, userId: user.id },
        include: [{
            model: Product,
            as: 'product',
            attributes: ['id', 'name', 'price']
        }]
    })

    return res.status(201).json({
        success: true,
        data: newCart
    })
}

const update = async (req, res, next) => {
    const cart = await Cart.findByPk(req.params.id)
    if (!cart) {
        return res.status(400).json({
            success: false,
            message: 'Invalid cartId'
        })
    }

    const result = await Cart.update(req.body, { where: { id: req.params.id } })
    return getById(req, res, next)
}

const remove = async (req, res, next) => {
    const cart = await Cart.findByPk(req.params.id)
    if (!cart) {
        return res.status(400).json({
            success: false,
            message: 'Invalid cartId'
        })
    }

    const result = await Cart.destroy({ where: { id: req.params.id } })
    return res.status(200).json({
        success: true
    })
}

const removeByUserId = async (req, res, next) => {
    const { data } = JWTService.decodeAccessToken(req.session.userToken.accessToken)

    const result = await Cart.destroy({ where: { userId: data.id } })
    return res.status(200).json({
        success: true
    })
}

module.exports = {
    getAllByUserId,
    create,
    update,
    remove,
    removeByUserId
}