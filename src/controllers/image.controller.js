const fs = require("fs")
const { Image, Product } = require("../models/index")

const getAllByProId = async (req, res, next) => {
    const product = await Product.findByPk(req.params.id)
    if (!product) {
        return res.status(400).json({
            success: false,
            message: 'Invalid proId'
        })
    }

    const images = await Image.findAll({
        where: { proId: req.params.id },
        include: [{
            model: Product,
            as: 'product',
            attributes: ['id', 'name']
        }]
    })

    return res.status(200).json({
        success: true,
        data: images
    })
}

const uploadGallery = async (req, res, next) => {
    const product = await Product.findByPk(req.params.id)
    if (!product) {
        req.files.forEach(file => {
            if (file)
                fs.unlinkSync(file.path)
        })

        return res.status(400).json({
            success: false,
            message: 'Invalid proId'
        })
    }

    req.files.forEach(async (file) => {
        if (file) {
            const image = {
                imgSrc: `/images/${file.filename}`,
                proId: product.id
            }
            await Image.create(image)
        }
    })

    return res.status(201).json({
        success: true
    })
}

const uploadImage = async (req, res, next) => {
    const path = `/images/${req.files[0].filename}`
    return res.status(200).json({
        success: true,
        path: path
    })
}

module.exports = {
    getAllByProId,
    uploadGallery,
    uploadImage
}