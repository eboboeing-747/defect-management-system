import { ref, type Ref, computed } from 'vue'
import { defineStore } from 'pinia';
import { type UserRef, type User, EMPTY_PFP } from '@/helpers/User.ts'

export const useUserDataStore = defineStore('userdata', () => {
    const login: Ref<string> = ref('');
    const firstName: Ref<string> = ref('');
    const middleName: Ref<string> = ref('');
    const lastName: Ref<string> = ref('');
    const pfpPath: Ref<string> = ref(EMPTY_PFP);
    const sex: Ref<boolean> = ref(false);
    const role: Ref<string> = ref('');
    const isLogged: Ref<boolean> = ref(false);

    function init(user: User): void {
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
    }

    function destruct(): void {
        login.value = '';
        firstName.value = '';
        middleName.value = '';
        lastName.value = '';
        pfpPath.value = '';
        sex.value = false;
        role.value = '';
    }

    return { login, firstName, middleName, lastName, pfpPath, sex, role, isLogged, init, destruct }
})
