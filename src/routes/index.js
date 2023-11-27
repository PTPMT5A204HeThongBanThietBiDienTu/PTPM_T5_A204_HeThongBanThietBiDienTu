const roleRouter = require("./role.route")
const categoryRouter = require("./category.route")
const brandRouter = require("./brand.route")
const productRouter = require("./product.route")
const specificationRouter = require("./specification.route")
const cartRouter = require("./cart.route")
const userRouter = require("./user.route")
const authRouter = require("./auth.route")
const billRouter = require("./bill.route")
const billProRouter = require("./billproduct.route")
const imageRouter = require("./image.route")
const mailRouter = require("./nodemailer.route")
const { createProxyMiddleware } = require("http-proxy-middleware")

const useRouter = (app) => {
    app.use('/api/v1/role', roleRouter)
    app.use('/api/v1/category', categoryRouter)
    app.use('/api/v1/brand', brandRouter)
    app.use('/api/v1/product', productRouter)
    app.use('/api/v1/specification', specificationRouter)
    app.use('/api/v1/cart', cartRouter)
    app.use('/api/v1/user', userRouter)
    app.use('/api/v1/auth', authRouter)
    app.use('/api/v1/bill', billRouter)
    app.use('/api/v1/billPro', billProRouter)
    app.use('/api/v1/image', imageRouter)
    app.use('/api/v1/sendmail', mailRouter)
    app.use('/api', createProxyMiddleware({
        target: 'https://vapi.vnappmob.com',
        changeOrigin: true,
        onProxyRes: function (proxyRes, req, res) {
            proxyRes.headers['Access-Control-Allow-Origin'] = 'http://localhost:3000';
        }
    }));
}

module.exports = useRouter