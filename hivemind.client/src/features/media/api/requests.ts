import type PaginatedDataResponse from '../../../utilities/PaginatedData';
import type { Media } from '../types';

export const getMedia = async (): Promise<Media[]> => {
    const response = await fetch('/api/mediaitems', {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' },
        body: null
    });

    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.mediaItems;
};


export const search = async (searchTerm: string, page: number, count: number): Promise<PaginatedDataResponse<Media>> => {
    let url = "/api/mediaitems";

    url += `?search=${searchTerm}&count=${count}&page=${page}`;

    const response = await fetch(url);

    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return {data: data.mediaItems, dataCount: data.dataCount} as PaginatedDataResponse<Media>;
};