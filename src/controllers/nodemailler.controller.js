const nodemailer = require('nodemailer')

const sendMail = async (req, res, next) => {
    const { name, email, content } = req.body
    const transporter = nodemailer.createTransport({
        service: 'gmail',
        auth: {
            user: '24072002ts@gmail.com',
            pass: 'zyuc lpix psfg jzwq'
        }
    })

    const mailOptions = {
        from: email,
        to: '24072002ts@gmail.com',
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