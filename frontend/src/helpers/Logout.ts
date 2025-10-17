import { Host } from '@/helpers/Host';
import { useUserDataStore } from '@/stores/userdata';

export async function Logout(): Promise<void> {
    const params: RequestInit = {
        method: 'GET',
        mode: 'cors',
        credentials: 'include'
    };

    const res: Response = await fetch(`${Host.getHost()}/User/Logout`, params);

    switch (res.status) {
        case 200:
            const userdata = useUserDataStore();
            userdata.LogOut();
            return;
        default:
            alert('failed to log out');
    }
}
