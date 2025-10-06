import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'
import MainView from '@/views/MainView.vue'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: 'Home',
            component: MainView
        },
        {
            path: '/login',
            name: 'LogIn',
            component: LoginView
        },
        {
            path: '/register',
            name: 'Register',
            component: RegisterView
        }
    ]
})

export default router
