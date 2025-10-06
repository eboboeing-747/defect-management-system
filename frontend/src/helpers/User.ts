import { type Ref } from 'vue';

export const EMPTY_PFP = '/empty-pfp.png';

export interface UserRef {
    login: Ref<string>,
    firstName: Ref<string>,
    middleName: Ref<string>,
    lastName: Ref<string>,
    pfpPath: Ref<string>,
    sex: Ref<boolean>
    role: Ref<string>,
    isLogged: Ref<boolean>
}

export interface User {
    login: string,
    firstName: string,
    middleName: string,
    lastName: string,
    pfpPath: string,
    sex: boolean
    role: string,
    isLogged: boolean
}
