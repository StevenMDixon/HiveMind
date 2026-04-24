import { ApiClient } from '../../../Api/ApiClient';

import { useState, Suspense, use, } from 'react'

import type { QueryScheduleItem, ScheduleItem } from '../../../Types/Schedule';
import type { EnumOptionsObj } from '../../../Types/General'; 
import type { Query } from '../../../Types/Query';

import ErrorBoundary from "../../../Components/ErrorBoundary";

import CustomForm from '../../Components/CustomForm';
import { type CustomFormField } from '../../Components/FormFields';

import { Container, AppBar, Toolbar, Button } from "@mui/material";

import { useParams, useNavigate } from "react-router-dom";

import { type EditingGridColumns } from '../../Components/EditingGrid';
import EditingGrid from '../../Components/EditingGrid';

import Box from '@mui/material/Box';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import AddIcon from '@mui/icons-material/Add';

import { useGlobalNotification } from '../../../Dashboard/useGlobalNotification';

import { toEnumOptions, getValueFromMappedObject } from "../../../Utilities/FormOptionsMapper"

interface ItemEditorProps {
    scheduleItemPromise: Promise<ScheduleItem>;
    queryTypePromise: Promise<EnumOptionsObj>;
    playoutTypePromise: Promise<EnumOptionsObj>;
    availableQueryPromise: Promise<Query[]>
    padToPromise: Promise<EnumOptionsObj>;
}

const ItemEditor = ({ scheduleItemPromise, queryTypePromise, playoutTypePromise, availableQueryPromise, padToPromise }: ItemEditorProps) => {
    const queryTypes = use(queryTypePromise);
    const queries = use(availableQueryPromise);
    const playOutTypes = use(playoutTypePromise);
    const scheduleItemData = use(scheduleItemPromise);
    const padToData = use(padToPromise); 

    const { showNotification } = useGlobalNotification();

    const getQueryName = (id: number) => {
        const foundQuery = queries.find(x => x.queryId == id);
        return foundQuery?.queryName ?? 'none';
    }

    const [scheduleItem, setScheduleItem] = useState(scheduleItemData);
    const [scheduleQueryItems, setScheduleQueryItems] = useState(scheduleItemData.queries);

    const scheduleItemFields = [
        { name: 'name', display: "Name", type: "Text", initialValue: scheduleItem.name },
        { name: 'type', display: "Schedule Item Type", type: "Radio", initialValue: scheduleItem.type, options: ["Generic", "Block"] }
    ] as CustomFormField[];

    const columns = [
        { name: 'queryId', display: 'Query', initialValue: 0, type: 'Select', format: (e: number) => getQueryName(e), options: toEnumOptions(queries, "queryId", "queryName", true)},
        { name: 'queryType', display: 'Assigned Type', initialValue: 0, type: 'Select', format: (e: number) => getValueFromMappedObject(queryTypes, e), options: queryTypes },
        { name: 'playoutType', display: 'Playout Type', initialValue: 0, type: 'Select', format: (e: number) => getValueFromMappedObject(playOutTypes, e), options: playOutTypes },
        { name: 'playDuration', display: 'Duration', initialValue: 0, type: 'Number', validator: (n: number) => n > -1 },
        { name: 'playCount', display: 'Count', initialValue: 0, type: 'Number', validator: (n: number) => n > -1 },
        { name: 'padTo', display: 'Pad To', initialValue: 0, type: 'Select', format: (e: number) => getValueFromMappedObject(padToData, e), options: padToData }
    ] as EditingGridColumns<QueryScheduleItem>[]

    const addItem = async () => {
        const currentIndex = scheduleQueryItems.length + 1;
        const newItem = { queryScheduleItemId: currentIndex * -1, scheduleItemId: scheduleItem.scheduleItemId, queryId: 0, queryType: 0, playoutType: 0, index: currentIndex, playDuration: 0, padTo: 0, playCount: 0 } as QueryScheduleItem
        setScheduleQueryItems([...scheduleQueryItems, newItem]);
    }

    const removeItem = (scheduleQueryItem: QueryScheduleItem) => {
        setScheduleQueryItems(scheduleQueryItems.filter(x => x.queryScheduleItemId != scheduleQueryItem.queryScheduleItemId));
    }

    const handleSaveScheduleQueryItem = (scheduleQueryItem: QueryScheduleItem) => {
        const itemIdx = scheduleQueryItems.findIndex(item => item.queryScheduleItemId == scheduleQueryItem.queryScheduleItemId)
        const updated = [...scheduleQueryItems]
        updated[itemIdx] = scheduleQueryItem

        setScheduleQueryItems(updated);
    }

    const handleUpdateScheduleItem = async (editedScheduleItem: ScheduleItem) => {
        const updatedScheduleItem = { ...editedScheduleItem, queries: scheduleQueryItems }
        const response = await ApiClient.updateScheduleItem(updatedScheduleItem);

        if (response.ok) {
            setScheduleItem(updatedScheduleItem);
            showNotification("Schedule Item Saved Successfully", "success")
        } else {
            showNotification("Error Saving Schedule Item", "error")
        }
    }
    return (
        <Container sx={{ mt: "1.5em", p: "1em" }}>
            <CustomForm title="Editing Schedule Item" save={handleUpdateScheduleItem} initialValue={scheduleItem} fields={scheduleItemFields}>
                <Box>
                    <Stack direction="row" justifyContent="space-between" sx={{ m: 1, p: 1 }}>
                        <Typography variant="h6" >Schedule Item Slots:</Typography>
                        < IconButton onClick={addItem} >
                            <AddIcon />
                        </IconButton>
                    </Stack>
                    <EditingGrid gridItems={scheduleQueryItems} gridFieldColumns={columns} deleteItem={removeItem} saveItem={handleSaveScheduleQueryItem} />
                </Box>
            </CustomForm>
        </Container>
    );
}


const ScheduleItemEdit = () => {

    const { itemId } = useParams();
    const [availableQueries] = useState(() => ApiClient.fetchQueries());
    const [availablePlayoutTypes] = useState(() => ApiClient.fetchPlayoutTypes());
    const [availableQueryTypes] = useState(() => ApiClient.fetchQueryTypes());
    const [availablePadTo] = useState(() => ApiClient.fetchPadToOptions());

    const correctedId = itemId ?? ""

    const [scheduleItem] = useState(() => ApiClient.fetchScheduleItem(Number(correctedId)));

    const navigate = useNavigate();

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Editing Schedule Item: {itemId}
                    </Typography>
                    <Button onClick={() => navigate(-1) }>
                        Back
                    </Button>
                </Toolbar>
            </AppBar>
            <ErrorBoundary
                fallback={(error) => (
                    <p>{error.message}</p>
                )}>
               <Suspense>
                    <ItemEditor scheduleItemPromise={scheduleItem} availableQueryPromise={availableQueries} playoutTypePromise={availablePlayoutTypes} queryTypePromise={availableQueryTypes} padToPromise={availablePadTo} />
                </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default ScheduleItemEdit