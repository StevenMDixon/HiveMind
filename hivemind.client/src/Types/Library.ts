export interface Library {
    libraryId: number;
    libraryName: string;
    libraryPath: string;
    pathsToIgnore: string;
    libraryType: number;
    isProcessed: boolean;
}