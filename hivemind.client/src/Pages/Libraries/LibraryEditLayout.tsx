import { Container } from "@mui/material"
import type { Library } from '../../Types/Library';
import { use } from 'react';
import { useNavigate } from "react-router-dom";

import CustomForm from '../Components/CustomForm';
import { type CustomFormField } from '../Components/FormFields';

import { saveLibrary } from '../../Api/Libraries'; 

interface LibraryEditLayoutProps {
    libraryPromise: Promise<Library>
    libraryTypesPromise: Promise<string []>
}

const LibraryEditLayout = ({ libraryPromise, libraryTypesPromise }: LibraryEditLayoutProps) => {

    const library = use(libraryPromise);
    const libraryTypes = use(libraryTypesPromise);

    const navigate = useNavigate();

    const handleSaveLibrary = async (item: Library) => {
        const result = await saveLibrary(item);

        if (result.ok) navigate(-1);
    }

    const fields = [
        { name: 'libraryName', type: "Text", initialValue: library.libraryName },
        { name: 'libraryPath', type: "Text", initialValue: library.libraryPath },
        { name: 'libraryType', type: "Select", initialValue: library.libraryType, options: libraryTypes },
    ] as CustomFormField[];

    return (
        <Container sx={{ mt: 5 }}>
            <CustomForm title="test" save={handleSaveLibrary} initialValue={library} fields={fields} />
        </Container>
    )
}

export default LibraryEditLayout;