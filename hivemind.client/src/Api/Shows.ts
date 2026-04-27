import { type Show } from '../Types/Show';

export const fetchShows = async (): Promise<Show[]> => {
    const response = await fetch('/shows');

    if (response.ok) {
        const data = await response.json();
        return data.shows;
    }

    return [];
}