import { type Show } from '../types';

export const getShows = async (): Promise<Show[]> => {
    const response = await fetch('/api/shows');

    if (response.ok) {
        const data = await response.json();
        return data.shows;
    }

    return [];
}