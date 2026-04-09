export const fetchMedia = async (): Promise<Media[]> => {
    const response = await fetch('/mediaitems');

    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.mediaItems;
};