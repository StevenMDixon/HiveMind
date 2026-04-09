import type { Collection, QueryOption } from '../Types/Collections';

export const fetchCollection = async (collectionId: number): Promise<Collection> => {
    const response = await fetch('/collections/' + collectionId);

    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

export const fetchOptions = async (): Promise<QueryOption[]> => {
    const response = await fetch('/querysettings');

    if (!response.ok) {
        throw new Error(`Failed to query options: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.options;
};

export const fetchCollections = async () => {
    const response = await fetch('/collections');

    if (response.ok) {
        const data = await response.json();
        return data.collections;
    }
};

export const createCollection = async (collection: Collection) => {
    const response = await fetch('/collections', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ CollectionName: collection.collectionName }),
    });

    return response;
};

export const deleteCollection = async (collectionId: number) => {
    const response = await fetch(`/collections/${collectionId}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });

    return response;
};