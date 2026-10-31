import type PaginatedDataResponse from '../../../utilities/PaginatedData';
import type { EnumOptionsObj } from '../../../utilities/types';
import { type Show } from '../types';

export const getShows = async (): Promise<Show[]> => {
    const response = await fetch('/api/shows');

    if (response.ok) {
        const data = await response.json();
        return data.shows;
    }

    return [];
}

export const getShow = async (id: string | undefined): Promise<Show> => {
    const response = await fetch('/api/shows/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch show: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.show;
};

export const createShow = async(showInputs: Show) => {
    const response = await fetch('/api/shows', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(showInputs),
    });

    return response;
}

export const updateShow = async (item: Show) => {

    console.log(JSON.stringify({name: item.name, rating: item.rating}))
    const result = await fetch('/api/shows/' + item.id, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({name: item.name, rating: item.rating}),
    });

    return result;
}



export const getRatingOptions = async (): Promise<EnumOptionsObj> => {
    const response = await fetch('/api/enums/ratings');

    if (!response.ok) throw new Error(`Failed to fetch rating options: ${response.status} ${response.statusText}`);

    const data = await response.json();

    return data.types;
};

export const search = async (searchTerm: string, page: number, count: number): Promise<PaginatedDataResponse<Show>> => {
    let url = "/api/shows";

    url += `?search=${searchTerm}&count=${count}&page=${page}`;

    const response = await fetch(url);

    if (!response.ok) {
        throw new Error(`Failed to fetch shows: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return {data: data.shows, dataCount: data.dataCount} as PaginatedDataResponse<Show>;
};