import type PaginatedDataResponse from '../../../utilities/PaginatedData';
import type { Library, EnumOptionsObj } from '../types';

export const getLibraries = async () => {
    const response = await fetch('/api/libraries');
    if (response.ok) {
        const data = await response.json();
        return data.libraries
    }
    return [];
};

export const getLibraryTypes = async (): Promise<EnumOptionsObj> => {
    const response = await fetch('/api/libraries/types');

    if (!response.ok) throw new Error(`Failed to fetch library types: ${response.status} ${response.statusText}`);

    const data = await response.json();

    return data.types;
};

export const createLibrary = async (libraryInputs: Library) => {
    const response = await fetch('/api/libraries', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(libraryInputs),
    });

    return response;
};

export const deleteLibrary = async (libraryId: number) => {
    const response = await fetch(`/api/libraries/${libraryId}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });

    return response;
};

export const getLibrary = async (id: string | undefined): Promise<Library> => {
    const response = await fetch('/api/libraries/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch library: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

export const updateLibrary = async (item: Library) => {
    const result = await fetch('/api/libraries/' + item.id, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ LibraryName: item.name, LibraryPath: item.path, PathsToIgnore: item.pathsToIgnore, LibraryType: item.type }),
    });

    return result;
}

export const reprocessLibrary = async (libraryId: number) => {
    const result = await fetch('/api/libraries/reprocess/' + libraryId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' }
    });

    return result;
}

export const search = async (searchTerm: string, page: number, count: number): Promise<PaginatedDataResponse<Library>> => {
    let url = "/api/libraries";

    url += `?search=${searchTerm}&count=${count}&page=${page}`;

    const response = await fetch(url);

    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return {data: data.libraries, dataCount: data.dataCount} as PaginatedDataResponse<Library>;
};