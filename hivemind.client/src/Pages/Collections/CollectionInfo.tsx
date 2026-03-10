import type { Collection } from './types';
import TextField from '@mui/material/TextField';
import Divider from '@mui/material/Divider';

export const CollectionInfo = ({ collection }: { collection: Collection | undefined }) => {
    return (
        <>
            <p>Collection Info:</p>
            <Divider sx={{ mb: 3 }} />
            {collection &&
                <TextField id="outlined-basic" label="Collection Name" variant="outlined" defaultValue={collection.collectionName} />
            }
        </>
    );
};
