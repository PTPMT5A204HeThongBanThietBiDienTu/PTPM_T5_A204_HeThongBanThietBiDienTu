const joi = require("joi")

const validateRole = (data) => {
    const dataSchema = joi.object({
        name: joi.string()
    })
    return dataSchema.validate(data)
}

const validateCategory = (data) => {
    const dataSchema = joi.object({
        name: joi.string()
    })
    return dataSchema.validate(data)
}

const validateBrand = (data) => {
    const dataSchema = joi.object({
        name: joi.string()
    })
    return dataSchema.validate(data)
}

const validateProduct = (data) => {
    const dataSchema = joi.object({
        name: joi.string(),
        price: joi.number(),
        quantity: joi.number(),
        description: joi.string(),
        catId: joi.string(),
        braId: joi.string()
    })
    return dataSchema.validate(data)
}

const validateUser = (data) => {
    const dataSchema = joi.object({
        name: joi.string(),
        email: joi.string().email(),
        address: joi.string(),
        phone: joi.string().length(10).pattern(/^[0-9]+$/).messages({ 'string.pattern.base': 'Phone number must have 10 digits' }),
        password: joi.string().length(5),
        roleId: joi.string()
    })
    return dataSchema.validate(data)
}

const validateRegister = (data) => {
    const dataSchema = joi.object({
        name: joi.string().required(),
        email: joi.string().email().required(),
        password: joi.string().length(5).required()
    })
    return dataSchema.validate(data)
}


const validateLogin = (data) => {
    const dataSchema = joi.object({
        email: joi.string().email().required(),
        password: joi.string().length(5).required()
    })
    return dataSchema.validate(data)
}

const validateSpecification = (data) => {
    const dataSchema = joi.object({
        proId: joi.string().required(),
        attributeName: joi.string().required(),
        attributeValue: joi.string().required()
    })
    return dataSchema.validate(data)
}

const validateInsertCart = (data) => {
    const dataSchema = joi.object({
        proId: joi.string().required()
    })
    return dataSchema.validate(data)
}

const validateUpdateCart = (data) => {
    const dataSchema = joi.object({
        quantity: joi.number().min(1).required()
    })
    return dataSchema.validate(data)
}

const validateBill = (data) => {
    const objectSchema = joi.object({
        proId: joi.string().required(),
        quantity: joi.number().min(1).required(),
        price: joi.number().required()
    })

    const dataSchema = joi.object({
        products: joi.array().items(objectSchema).required()
    })
    return dataSchema.validate(data)
}

module.exports = {
    validateRole,
    validateBrand,
    validateCategory,
    validateProduct,
    validateUser,
    validateRegister,
    validateLogin,
    validateSpecification,
    validateInsertCart,
    validateUpdateCart,
    validateBill
}