import { type Ref } from 'vue';

export const EMPTY_PFP = '/empty-pfp.png';
export type Role = 'engineer' | 'manager' | 'supervisor' | 'log in to see role';

export interface UserRef {
    login: Ref<string>,
    firstName: Ref<string>,
    middleName: Ref<string>,
    lastName: Ref<string>,
    pfpPath: Ref<string>,
    sex: Ref<boolean>
    role: Ref<Role>,
    isLogged: Ref<boolean>
}

export interface User {
    login: string,
    firstName: string,
    middleName: string,
    lastName: string,
    pfpPath: string,
    sex: boolean
    role: Role,
    isLogged: boolean
}

export interface UserCredentials {
    login: string,
    password: string
}

export interface UserRegister extends UserCredentials {
    firstName: string,
    middleName: string,
    lastName: string,
    role: Role,
    sex: boolean
}
