import joi from "joi"

export const validateRole = (data) => {
    const dataSchema = joi.object({
        name: joi.required()
    })
    return dataSchema.validate(data)
}