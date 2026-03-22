import { Container } from "@mui/material"
import type { Library } from './types';
import { use } from 'react';
import { useNavigate } from "react-router-dom";


import CustomEditForm, { type CustomFormField } from '../Components/EditCustomForm';

interface LibraryEditLayoutProps {
    libraryPromise: Promise<Library>
    libraryTypesPromise: Promise<string []>
}

const LibraryEditLayout = ({ libraryPromise, libraryTypesPromise }: LibraryEditLayoutProps) => {

    const library = use(libraryPromise);
    const libraryTypes = use(libraryTypesPromise);

    const navigate = useNavigate();

    console.log(library.libraryType, libraryTypes)

    const saveLibrary = async (item: Library) => {
        const result = await fetch('/libraries/' + item.libraryId, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ LibraryName: item.libraryName, LibraryPath: item.libraryPath, LibraryType: item.libraryType }),
        });

        if (result.ok) navigate(-1);
    }

    const fields = [
        { name: 'libraryName', type: "Text", initialValue: library.libraryName },
        { name: 'libraryPath', type: "Text", initialValue: library.libraryPath },
        { name: 'libraryType', type: "Select", initialValue: library.libraryType, options: libraryTypes },
    ] as CustomFormField[];

    return (
        <Container sx={{ mt: 5 }}>
            <CustomEditForm title="test" save={saveLibrary} initialValue={library} fields={fields} />
        </Container>
    )
}

export default LibraryEditLayout;