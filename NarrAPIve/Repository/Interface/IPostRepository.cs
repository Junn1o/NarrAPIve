using NarrAPIve.Model.Domain;
using NarrAPIve.Model.DTO;

namespace NarrAPIve.Repository.Interface
{
    public interface IPostRepository
    {
        PostResultDTO GetFullPost(int pageNumber, int pageSize);
        PostWithIdDTO PostWithId(Guid postId);
        PostWithUserIdDTO PostWithUserId(Guid userId);
        VolumeDetailWithIdDTO VolumeDetailWithId(Guid volumeId);
        PostRequestFormDTO AddPost(PostRequestFormDTO addpostDTO);
        PostRequestFormDTO UpdatePost(Guid postId, PostRequestFormDTO updatepostDTO);
        post? DeletePost(Guid postId);
        VolumeRequestFormDTO AddVolume(VolumeRequestFormDTO addvolumeDTO);
        VolumeRequestFormDTO UpdateVolume(Guid volumeId, VolumeRequestFormDTO updatevolumeDTO);
        volume? DeleteVolume(Guid volumeId);
        ChapterRequestFormDTO AddChapter(ChapterRequestFormDTO addchapterDTO);
        ChapterRequestFormDTO UpdateChapter(Guid chapterId, ChapterRequestFormDTO updatechapterDTO);
        chapter? DeleteChapter(Guid chapterId);
        ChapterImageRequestFormDTO AddChapterImage(ChapterImageRequestFormDTO addchapterimageDTO);
        ChapterImageRequestFormDTO UpdateChapterImage(ChapterImageRequestFormDTO updatechapterimageDTO);
        string DeleteChapterImage(string chapterImagePath);
    }
}
