import { Container, AppBar, Toolbar, Typography, Button } from "@mui/material";
import Stack from '@mui/material/Stack';

import { useNavigate, useParams } from "react-router-dom";
import { getRandomMediabyType } from '../api/requests';

import MediaTagging from '../components/MediaTagging';
import { mediaQueryOptions, tagCategoryOptions, tagCategoryRequirementsOptions, tagOptions } from '../api/queryOptions';
import { useQuery, useQueryClient } from '@tanstack/react-query';

const MediaQueuePage = () => {
    const { id } = useParams();
  
    const navigate = useNavigate();
    const queryClient = useQueryClient(); // Access the cache client

    const { data: media } = useQuery(mediaQueryOptions(id ?? '0'));
    const { data: tagCategories } = useQuery(tagCategoryOptions());
    const { data: tagRequirements } = useQuery(tagCategoryRequirementsOptions());
    const { data: tags } = useQuery(tagOptions());

    const handleNavigate = async () => {
        const category = media?.libraryType ?? "Other";
        try {
            // Fetch data imperatively. This respects caching rules.
            const data = await queryClient.fetchQuery({
            queryKey: ['Media'],
            queryFn: () => getRandomMediabyType(category),
            staleTime: 0, 
            });

            navigate('/queue/' + data);
            // You can now safely use the data right here in the handler
        } catch (error) {
            console.error('Fetch failed:', error);
        }
    }

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Tagging Media: {media?.id}
                    </Typography>
                    <Button onClick={() => navigate(-1)}> Back</Button>
                </Toolbar>
            </AppBar>
            <Stack sx={{justifyContent:'center', mt: '1em'}} direction="row">
                
                {media && tagRequirements && tagCategories && tags && <MediaTagging media={media} tagCategories={tagRequirements} allCategories={tagCategories} availableTags={tags} next={handleNavigate}/>}
                
            </Stack>
        </Container>
    )
}


export default MediaQueuePage;