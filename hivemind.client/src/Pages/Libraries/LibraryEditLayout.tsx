import { Container } from "@mui/material"
import type { Library } from '../../Types/Library';
import type { EnumOptionsObj } from '../../Types/General'; 

import { use } from 'react';

import CustomForm from '../Components/CustomForm';
import { type CustomFormField } from '../Components/FormFields';
import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';

import { ApiClient } from '../../Api/ApiClient';

interface LibraryEditLayoutProps {
    libraryPromise: Promise<Library>
    libraryTypesPromise: Promise<EnumOptionsObj>
}

const LibraryEditLayout = ({ libraryPromise, libraryTypesPromise }: LibraryEditLayoutProps) => {

    const library = use(libraryPromise);
    const libraryTypes = use(libraryTypesPromise);

    console.log(libraryTypes)

    const { showNotification } = useGlobalNotification();

    const handleSaveLibrary = async (item: Library) => {
        const result = await ApiClient.saveLibrary(item);

        if (result.ok) {
            showNotification("Library Edited Successfully", "success")
        } else {
            showNotification("Error Editing Library", "error")
        }
    }

    const fields = [
        { name: 'libraryName', display: "Name", type: "Text", initialValue: library.libraryName },
        { name: 'libraryPath', display: "Path", type: "Text", initialValue: library.libraryPath },
        { name: 'libraryType', display: "Type", type: "Select", initialValue: library.libraryType, options: libraryTypes },
    ] as CustomFormField[];

    return (
        <Container sx={{ mt: 5 }}>
            <CustomForm title="test" save={handleSaveLibrary} initialValue={library} fields={fields} />
        </Container>
    )
}

export default LibraryEditLayout;