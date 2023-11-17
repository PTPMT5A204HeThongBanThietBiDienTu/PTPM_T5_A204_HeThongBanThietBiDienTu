const { Bill, Product, BillProduct, Customer } = require("../models/index")
const JWTService = require("../services/jwt.service")
const BillStatus = require("../models/BillStatus")

const getAllByUserId = async (req, res, next) => {
    const { data } = JWTService.decodeAccessToken(req.session.userToken.accessToken)

    const bills = await Bill.findAll({
        where: { userId: data.id },
        include: {
            model: Customer,
            as: 'customer'
        }
    })

    return res.status(200).json({
        success: true,
        data: bills
    })
}

const create = async (req, res, next) => {
    if (req.body.products.length == 0) {
        return res.status(400).json({
            success: false,
            message: 'products is not allowed to be empty'
        })
    }

    const { data } = JWTService.decodeAccessToken(req.session.userToken.accessToken)
    let customer = {
        name: req.body.name,
        phone: req.body.phone,
        address: req.body.address
    }

    const cusNew = await Customer.create(customer)
    const bill = await Bill.create({ userId: data.id, cusId: cusNew.id })

    let total = bill.total
    for (let product of req.body.products) {
        const pro = await Product.findByPk(product.proId)
        if (!pro) {
            await Bill.destroy({ where: { id: bill.id } })

            return res.status(400).json({
                success: false,
                message: 'Invalid proId'
            })
        }

        total += (product.price * product.quantity)
        const billPro = {
            proId: product.proId,
            price: product.price,
            quantity: product.quantity,
            billId: bill.id
        }

        await BillProduct.create(billPro)
    }

    await Bill.update({ total: total }, { where: { id: bill.id } })

    const billNew = await Bill.findByPk(bill.id, {
        include: {
            model: Customer,
            as: 'customer',
        }
    })

    return res.status(200).json({
        success: true,
        data: billNew
    })
}

module.exports = {
    getAllByUserId,
    create
}