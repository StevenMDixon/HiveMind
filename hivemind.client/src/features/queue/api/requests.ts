import type { EnumOptionsObj } from '../../libraries/types';
import type { Media } from '../../media/types';
import type { Tag } from '../../tags/types';
import { type CategoryItem, type QueueItem } from '../types';

const queueUrl = '/api/TagQueue/';

export const getQueues = async (): Promise<QueueItem[]> => {
    const response = await fetch(queueUrl);

    if (response.ok) {
        const data = await response.json();
        return data.queues;
    }

    return [];
}

export const getMedia = async (id: string): Promise<Media> => {
    const response = await fetch('/api/mediaitems/' + id, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' },
        body: null
    });

    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

export const getRandomMediabyType = async (type: string): Promise<Media> => {
    const response = await fetch('/api/mediaitems/type/' + type, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' },
        body: null
    });

    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

export const getCategoryOptions = async (): Promise<EnumOptionsObj> => {
    const response = await fetch('/api/tags/categories');

    if (!response.ok) throw new Error(`Failed to fetch category options: ${response.status} ${response.statusText}`);

    const data = await response.json();

    return data.categories;
};

export const getCategory = async (): Promise<CategoryItem[]> => {
    const response = await fetch('/api/tagqueue/tag-categories');

    if (!response.ok) throw new Error(`Failed to fetch categories: ${response.status} ${response.statusText}`);

    const data = await response.json();
    return data;
};

export const updateTags = async (media: Media) => {
    return await fetch('/api/mediaitems/' + media.id, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ tag: media.tags }),
    });
}

const tagUrl = '/api/tags';

export const getTags = async (): Promise<Tag[]> => {
    const response = await fetch(tagUrl);

    if (response.ok) {
        const data = await response.json();
        return data.tags;
    }

    return [];
}