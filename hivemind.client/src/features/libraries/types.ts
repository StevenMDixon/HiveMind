export interface Library {
    libraryId: number;
    libraryName: string;
    libraryPath: string;
    pathsToIgnore: string;
    libraryType: number;
    isProcessed: boolean;
}

export interface EnumOptionsObj {
    [key: number]: string | number | boolean; // Keys are numbers, values are strings
}