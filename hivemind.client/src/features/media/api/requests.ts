import type { Media } from '../types';

export const getMedia = async (): Promise<Media[]> => {
    const response = await fetch('/api/mediaitems');

    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.mediaItems;
};