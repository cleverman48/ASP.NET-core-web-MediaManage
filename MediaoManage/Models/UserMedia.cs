using System.ComponentModel.DataAnnotations;

namespace MediaoManage.Models
{
    public class UserMedia
    {
        public int Id { get; set; }
        [Display(Name = "登録者")]
        public string? user_id { get; set; }
        [Display(Name = "コンテナ")]
        public int? container_id { get; set; }
        [Display(Name = "ファイル名")]
        public string? media_file_name { get; set; }
        [Display(Name = "拡張子")]
        public string? media_file_type { get; set; }
        [Display(Name = "動画のストレージパス")]
        public string? media_url { get; set; }
        public string? media_small_url { get; set; }
        [Display(Name = "サムネイル画像")]
        public string? media_thumbnail_url { get; set; }
        [Display(Name = "アップロードした日")]
        [DataType(DataType.Date)]
        public DateTime media_uploaded { get; set; }
        [Display(Name = "タイトル　")]
        public string? media_title { get; set; }
        [Display(Name = "説明")]
        public string? media_description { get; set; }
        [Display(Name = "タグ")]
        public string? media_tags { get; set; }

        public string? Tags { get; set; }
        public string? ProjectTags { get; set; }
        [Display(Name = "データの作成日")]
        [DataType(DataType.Date)]
        public DateTime media_created { get; set; }
        public string? media_originaltags { get; set; }
        public string? media_aitags { get; set; }
        public int media_status { get; set; }
        [Display(Name = "キーワード")]
        public string? media_searchtext { get; set; }
        [Display(Name = "変更日")]
        [DataType(DataType.Date)]
        public DateTime media_modified { get; set; }
        [Display(Name = "削除日")]
        [DataType(DataType.Date)]
        public DateTime media_deleted { get; set; }
    }
}
