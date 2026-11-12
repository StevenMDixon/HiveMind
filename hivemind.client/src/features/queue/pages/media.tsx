import { Container, AppBar, Toolbar, Typography, Button } from "@mui/material";
import Stack from '@mui/material/Stack';

import { useNavigate, useParams } from "react-router-dom";

import MediaTagging from '../components/MediaTagging';
import { mediaQueryOptions, tagCategoryOptions, tagCategoryRequirementsOptions, tagOptions } from '../api/queryOptions';

import { useQuery } from '@tanstack/react-query';

const MediaQueuePage = () => {
  const { id } = useParams();

    const navigate = useNavigate();
   
    const { data: media } = useQuery(mediaQueryOptions(id ?? '0'));
    const { data: tagCategories } = useQuery(tagCategoryOptions());
    const { data: tagRequirements } = useQuery(tagCategoryRequirementsOptions());
    const { data: tags } = useQuery(tagOptions());

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
                
                {media && tagRequirements && tagCategories && tags && <MediaTagging media={media} tagCategories={tagRequirements} allCategories={tagCategories} availableTags={tags} />}
                
            </Stack>
        </Container>
    )
}


export default MediaQueuePage;