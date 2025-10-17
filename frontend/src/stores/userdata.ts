import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia';
import { type Role, type User, EMPTY_PFP } from '@/helpers/User.ts'

const defaults: User = {
    login: '',
    firstName: '',
    middleName: '',
    lastName: '',
    pfpPath: EMPTY_PFP,
    sex: false,
    role: 'log in to see role',
    isLogged: false
}

export const useUserDataStore = defineStore('userdata', () => {
    const login: Ref<string> = ref(defaults.login);
    const firstName: Ref<string> = ref(defaults.firstName);
    const middleName: Ref<string> = ref(defaults.middleName);
    const lastName: Ref<string> = ref(defaults.lastName);
    const pfpPath: Ref<string> = ref(defaults.pfpPath);
    const sex: Ref<boolean> = ref(defaults.sex);
    const role: Ref<Role> = ref(defaults.role);
    const isLogged: Ref<boolean> = ref(defaults.isLogged);

    function LogIn(user: User): void {
        login.value = user.login;
        firstName.value = user.firstName;
        middleName.value = user.middleName;
        lastName.value = user.lastName;

        if (user.pfpPath == '')
            pfpPath.value = EMPTY_PFP;
        else
            pfpPath.value = user.pfpPath;

        sex.value = user.sex;
        role.value = user.role;

        isLogged.value = true;
    }

    function LogOut(): void {
        login.value = defaults.login;
        firstName.value = defaults.firstName;
        middleName.value = defaults.middleName;
        lastName.value = defaults.lastName;
        pfpPath.value = defaults.pfpPath;
        sex.value = defaults.sex;
        role.value = defaults.role;

        isLogged.value = false;
    }

    return { login, firstName, middleName, lastName, pfpPath, sex, role, isLogged, LogIn, LogOut }
})
