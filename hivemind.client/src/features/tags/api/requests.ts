import type PaginatedDataResponse from '../../../utilities/PaginatedData';
import type { EnumOptionsObj } from '../../../utilities/types';
import { type Tag } from '../types';

const tagUrl = '/api/tags/';

export const getTags = async (): Promise<Tag[]> => {
    const response = await fetch(tagUrl);

    if (response.ok) {
        const data = await response.json();
        return data.tags;
    }

    return [];
}

export const getTag = async (id: string | undefined): Promise<Tag> => {
    const response = await fetch(tagUrl + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch show: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

export const createTag = async(tagInputs: Tag) => {
    const response = await fetch(tagUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(tagInputs),
    });

    return response;
}

export const updateTag = async (item: Tag) => {
    const result = await fetch(tagUrl + item.id, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(item),
    });

    return result;
}

export const deleteTag = async(item: Tag) => {
    const response = await fetch(tagUrl + item.id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });

    return response;
}

export const getCategoryOptions = async (): Promise<EnumOptionsObj> => {
    const response = await fetch(tagUrl + 'categories');

    if (!response.ok) throw new Error(`Failed to fetch categories: ${response.status} ${response.statusText}`);

    const data = await response.json();

    return data.categories;
};

export const search = async (searchTerm: string, page: number, count: number): Promise<PaginatedDataResponse<Tag>> => {
    let url = tagUrl;

    url += `?search=${searchTerm}&count=${count}&page=${page}`;

    const response = await fetch(url);

    if (!response.ok) {
        throw new Error(`Failed to fetch shows: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return {data: data.tags, dataCount: data.dataCount} as PaginatedDataResponse<Tag>;
};