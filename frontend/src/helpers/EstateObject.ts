export interface IEstateObjectCard {
    id: string;
    imagePath: string;
    name: string;
    address: string;
}

export interface IEstateObjectCreate {
    id: string;
    filelist: File[];
    description: string;
    name: string;
    address: string;
}

export interface IEstateObject {
    images: string[];
    description: string;
    name: string;
    address: string;
}
