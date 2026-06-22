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
        body: JSON.stringify({ LibraryName: libraryInputs.libraryName, LibraryPath: libraryInputs.libraryPath, PathsToIgnore: libraryInputs.pathsToIgnore, LibraryType: libraryInputs.libraryType }),
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
    const result = await fetch('/api/libraries/' + item.libraryId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ LibraryName: item.libraryName, LibraryPath: item.libraryPath, PathsToIgnore: item.pathsToIgnore, LibraryType: item.libraryType }),
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