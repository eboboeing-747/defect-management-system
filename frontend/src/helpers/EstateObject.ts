export interface EstateObjectCard {
    id: string;
    imagePath: string;
    name: string;
    address: string;
}

export interface EstateObjectCreate {
    id: string;
    filelist: File[];
    description: string;
    name: string;
    address: string;
}

export interface EstateObject {
    images: string[];
    description: string;
    name: string;
    address: string;
}
