export interface Show {
    showId: number;
    name: string;
}

export interface Tag {
    tagId: number;
    tagName: string;
}

export interface Media {
    mediaItemId: number;
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
}