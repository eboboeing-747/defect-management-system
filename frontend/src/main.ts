import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import { Host } from './helpers/Host'
import { useUserDataStore } from './stores/userdata'

const app = createApp(App)

app.use(createPinia())
app.use(router)

Host.init();

try {
    const opts: RequestInit = {
        method: 'GET',
        mode: 'cors',
        credentials: 'include'
    };

    const res = await fetch(`${Host.getHost()}/User/Authorize`, opts);

    switch (res.status) {
    case 200:
        const body = await res.json();
        const userdata = useUserDataStore();
        userdata.LogIn(body);
        break;
    case 401:
    case 403:
        console.log('unauthorized');
        break;
    default:
        console.log(res.status);
    }
} catch(error: unknown) {
    console.log(error);
}

app.mount('#app')
