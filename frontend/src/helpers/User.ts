import { type Ref } from 'vue';

export const EMPTY_PFP = '/empty-pfp.png';

export enum Role {
    ENGINEER = 'engineer',
    MANAGER = 'manager',
    SUPERVISOR = 'supervisor'
}

export interface IUserRef {
    login: Ref<string>,
    firstName: Ref<string>,
    middleName: Ref<string>,
    lastName: Ref<string>,
    pfpPath: Ref<string>,
    sex: Ref<boolean>
    role: Ref<Role>,
    isLogged: Ref<boolean>
}

export interface IUser {
    login: string,
    firstName: string,
    middleName: string,
    lastName: string,
    pfpPath: string,
    sex: boolean
    role: Role,
    isLogged: boolean
}

export interface IUserCredentials {
    login: string,
    password: string
}

export interface IUserRegister extends IUserCredentials {
    firstName: string,
    middleName: string,
    lastName: string,
    role: Role,
    sex: boolean
}
