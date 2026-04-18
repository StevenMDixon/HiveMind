import type { Query } from '../Types/Query';
import type { Media } from '../Types/Media';

export const fetchQuery = async (queryId: number): Promise<Query> => {
    const response = await fetch('/queries/' + queryId);
    
    if (!response.ok) {
        throw new Error(`Failed to fetch query: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

export const fetchTestQueryResults = async (queryId: number): Promise<Media[]> => {
    const response = await fetch('/queries/test/' + queryId);

    if (!response.ok) {
        throw new Error(`Failed to fetch query: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.mediaItems;
};

export const fetchQueries = async () => {
    const response = await fetch('/queries');
    if (response.ok) {
        const data = await response.json();
        return data.queries;
    }
};

export const createQuery = async (query: Query) => {
    const response = await fetch('/queries', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ QueryName: query.queryName }),
    });

    return response;
};

export const deleteQuery = async (queryId: number) => {
    const response = await fetch(`/queries/${queryId}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });

    return response;
};

export const saveQuery = async(query: Query) => {
    const result = await fetch('/queries/' + query.queryId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(query),
    });

    return result;
}