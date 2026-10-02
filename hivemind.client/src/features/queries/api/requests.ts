import type { Query } from '../types';
import type { Media } from '../../media/types';
import type { QuerySetting, QuerySettingItem } from "../types";


export const getQuery = async (queryId: number): Promise<Query> => {
    const response = await fetch('/api/queries/' + queryId);
    
    if (!response.ok) {
        throw new Error(`Failed to fetch query: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

export const getTestQueryResults = async (queryId: number): Promise<Media[]> => {
    const response = await fetch('/api/queries/test/' + queryId);

    if (!response.ok) {
        throw new Error(`Failed to fetch query: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.mediaItems;
};

export const getQueries = async () => {
    const response = await fetch('/api/queries');
    if (response.ok) {
        const data = await response.json();
        return data.queries;
    }
};

export const createQuery = async (query: Query) => {
    const response = await fetch('/api/queries', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ QueryName: query.queryName }),
    });

    return response;
};

export const deleteQuery = async (queryId: number) => {
    const response = await fetch(`/api/queries/${queryId}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });

    return response;
};

export const updateQuery = async(query: Query) => {
    const result = await fetch('/api/queries/' + query.queryId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(query),
    });

    return result;
}


export const getSettings = async (): Promise<QuerySetting[]> => {
    const response = await fetch('/api/enums/querysettings');

    if (!response.ok) {
        throw new Error(`Failed to query settings: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.options;
};

export const getOptions = async (): Promise<QuerySettingItem[]> => {
    const response = await fetch('/api/enums/queryoptions');

    if (!response.ok) {
        throw new Error(`Failed to query options: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    console.log(data)
    return data.options;
};