const { Bill, BillProduct, Product } = require("../models/index")

const getAllByBillId = async (req, res, next) => {
    const bill = await Bill.findByPk(req.params.id)
    if (!bill) {
        return res.status(400).json({
            success: false,
            message: 'Invalid billId'
        })
    }

    const billPro = await BillProduct.findAll({
        where: { billId: req.params.id },
        include: [{
            model: Product,
            as: 'product',
            attributes: ['id', 'name', 'img']
        }]
    })

    return res.status(200).json({
        success: true,
        data: billPro
    })
}

module.exports = {
    getAllByBillId
}