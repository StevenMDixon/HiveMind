export interface Library {
    id: number;
    name: string;
    path: string;
    pathsToIgnore: string;
    type: number;
    isProcessed: boolean;
}

export interface EnumOptionsObj {
    [key: number]: string | number | boolean; // Keys are numbers, values are strings
}