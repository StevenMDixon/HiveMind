export interface Show {
    id: number;
    name: string;
}

export interface Tag {
    id: number;
    name: string;
    category: number;
}

export interface Media {
    id: number;
    title: string;
    duration: number;
    libraryId: number;
    mediaType: string;
    filePath: string;
    width: string;
    height: string;
    resolution: string;
    episodeNumber: number;
    seasonNumber: number;
    show: Show;
    tags: Tag[];
    thumbnailPath: string;
}