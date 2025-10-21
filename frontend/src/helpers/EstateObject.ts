export interface IEstateObjectCard {
    id: string;
    imagePath: string;
    name: string;
    address: string;
}

export interface IEstateObject {
    id: string;
    filelist: File[];
    description: string;
    name: string;
    address: string;
}

export interface IEstateObjectView {
    images: string[];
    description: string;
    name: string;
    address: string;
}
