const nodemailer = require('nodemailer')
require('dotenv').config()

const sendMail = async (req, res, next) => {
    const { name, email, content } = req.body
    const transporter = nodemailer.createTransport({
        service: 'gmail',
        auth: {
            user: process.env.SMTP_USER,
            pass: process.env.SMTP_PASSWORD
        }
    })

    const mailOptions = {
        from: email,
        to: process.env.SMTP_USER,
        subject: name,
        html: content
    }

    await transporter.sendMail(mailOptions, (error, info) => {
        if (error) {
            return res.status(500).send(error.toString());
        }
        return res.status(200).json({
            success: true,
        })
    })
}
module.exports = {
    sendMail
}