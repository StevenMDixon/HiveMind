import type { Setting } from '../types';

import { useQuery } from '@tanstack/react-query';
import { settingsQueryOptions } from '../api/queryOptions';

import EditingGrid from '@/components/EditingGrid';
import { type EditingGridColumns } from '@/components/EditingGrid';

import Container from '@mui/material/Container';
import Header from '@/components/Header';

import { useSettingUpdateHook } from '../hooks/settingsHooks';


const SettingPage = () => {
    const { data: settings, isLoading } = useQuery(settingsQueryOptions());
    console.log(settings)

    const columns = [
        { name: 'name', display: 'Setting', initialValue: '', type: 'Text', disabled: true},
        { name: 'value', display: 'Value', initialValue: '', type: 'Text'},
    ] as EditingGridColumns<Setting>[]

    const { mutate: saveSetting } = useSettingUpdateHook();

    const handleUpdate = (setting: Setting) => saveSetting(setting);

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="System Settings">
            </Header>
            <EditingGrid gridItems={settings} gridFieldColumns={columns} saveItem={handleUpdate} isLoading={isLoading} />
        </Container>
    )
}

export default SettingPage;