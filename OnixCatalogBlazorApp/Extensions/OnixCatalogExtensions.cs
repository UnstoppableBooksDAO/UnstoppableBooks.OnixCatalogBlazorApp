using Newtonsoft.Json;

using OnixData.Version3;
using OnixData.Version3.Names;

using OnixCatalogBlazorApp.Models;

namespace OnixCatalogBlazorApp.Extensions
{
    public static class OnixCatalogExtensions
    {
        public const string OnixIdTypeNameDID = @"W3C CCG DID";

        public const string Onix3BasicMessageFormat = 
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<ONIXMessage release=""3.0"" xmlns=""http://ns.editeur.org/onix/3.0/reference""
			 xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
    {0}
	<Product>
		<DescriptiveDetail>
			<ProductForm>{1}</ProductForm>
            <Title language=""eng"">
                <TitleType>01</TitleType>
                <TitleText><![CDATA[{2}]]></TitleText>
            </Title>
            {3}
			<Language>
				<LanguageRole>01</LanguageRole>
				<LanguageCode>{4}</LanguageCode>
			</Language>
			<Subject>
				<MainSubject/>
				<SubjectSchemeIdentifier>10</SubjectSchemeIdentifier>
				<SubjectSchemeVersion>2017</SubjectSchemeVersion>
				<SubjectCode>{5}</SubjectCode>
			</Subject>
		</DescriptiveDetail>
		<PublishingDetail>
			<Publisher>
				<PublishingRole>01</PublishingRole>
				<PublisherName>{6}</PublisherName>
			</Publisher>
			<PublishingDate>
				<PublishingDateRole>01</PublishingDateRole>
				<Date dateformat=""00"">{7}</Date>
			</PublishingDate>
		</PublishingDetail>
	</Product>
</ONIXMessage>
         ";

        public const string Onix3HeaderFormat =
@"
	<Header>
		<Sender>
            {0}
			{1}
		</Sender>
		<SentDateTime>{2}</SentDateTime>
		<MessageNote><![CDATA[{3}]]></MessageNote>
	</Header>	
";

        public const string Onix3SenderIdFormat =
@"
			<SenderIdentifier>
				<SenderIDType>{0}</SenderIDType>
			    <IDTypeName>{1}</IDTypeName>
				<IDValue>{2}</IDValue>
			</SenderIdentifier>
";

        public const string Onix3SenderNameFormat =
@"
            <SenderName>{0}</SenderName>
";

        public const string Onix3BasicCntbFormat =
@"
			<Contributor>
				<ContributorRole>A01</ContributorRole>
                {0}
                {1}
			</Contributor>
";

        public const string Onix3BasicCntbIdFormat =
@"
				<NameIdentifier>
					<NameIDType>{0}</NameIDType>
					<IDTypeName>{1}</IDTypeName>
					<IDValue>{2}</IDValue>
				</NameIdentifier>
";

        public const string Onix3BasicCntbNameFormat =
@"
				<NamesBeforeKey>{0}</NamesBeforeKey>
				<KeyNames>{1}</KeyNames>
";

        public const string Onix3BasicCntbPersonNameFormat =
@"
                <PersonName>{0}</PersonName>
";

		public static string Serialize(this BookItem bookItem)
        {
			return JsonConvert.SerializeObject(bookItem);
		}

        public static string ToSimpleOnixString(this BookItem bookItem, string headerMsgNote = null)
        {
            var header      = String.Empty;
            var contribList = String.Empty;

            var cntbIds   = String.Empty;
            var cntbNames = String.Empty;

            var senderIds   = String.Empty;
            var senderNames = String.Empty;
				
            if (!String.IsNullOrEmpty(bookItem?.AuthorEthereumId))
            {
                cntbIds = String.Format(Onix3BasicCntbIdFormat
                                        , OnixNameIdentifier.CONST_NAME_TYPE_ID_PROP
                                        , OnixIdTypeNameDID
                                        , String.Format("did:ethr:{0}", bookItem.AuthorEthereumId));

                senderIds = String.Format(Onix3SenderIdFormat
                                          , OnixNameIdentifier.CONST_NAME_TYPE_ID_PROP
                                          , OnixIdTypeNameDID
                                          , String.Format("did:ethr:{0}", bookItem.AuthorEthereumId));
            }

            if (!String.IsNullOrEmpty(bookItem?.AuthorName))
            {
                cntbNames   = String.Format(Onix3BasicCntbPersonNameFormat, bookItem.AuthorName);
                senderNames = String.Format(Onix3SenderNameFormat, bookItem.AuthorName);
            }

            header = String.Format(Onix3HeaderFormat
                                    , senderIds
                                    , senderNames
                                    , DateTime.Now.ToString("YYYYMMDD")
                                    , headerMsgNote ?? String.Empty);

            contribList = String.Format(Onix3BasicCntbFormat, cntbIds, cntbNames);

            return String.Format(Onix3BasicMessageFormat
                                 , header
                                 , "DG"
                                 , bookItem?.Title
                                 , contribList
                                 , String.Empty
                                 , bookItem?.PrimaryBISAC
                                 , bookItem?.Publisher
                                 , String.Empty
                                );

        }

        public static string ToSimpleOnixString(this OnixProduct onixProduct, string headerMsgNote = null)
        {
            var header      = String.Empty;
            var contribList = String.Empty;

            if (onixProduct.PrimaryAuthor != null)
            {
                var cntbIds   = String.Empty;
                var cntbNames = String.Empty;

                var senderIds   = String.Empty;
                var senderNames = String.Empty;
				
                if (onixProduct.PrimaryAuthor.OnixNameIdList.Any())
                {
                    var firstNameId = onixProduct.PrimaryAuthor.OnixNameIdList[0];

                    cntbIds = String.Format(Onix3BasicCntbIdFormat
                                            , firstNameId.NameIDType
                                            , firstNameId.IDTypeName
                                            , firstNameId.IDValue);

                    senderIds = String.Format(Onix3SenderIdFormat
                                            , firstNameId.NameIDType
                                            , firstNameId.IDTypeName
                                            , firstNameId.IDValue);
                }

                if (!String.IsNullOrEmpty(onixProduct.PrimaryAuthor.OnixKeyNames))
                {
                    cntbNames = String.Format(Onix3BasicCntbNameFormat
                                              , onixProduct.PrimaryAuthor.OnixNamesBeforeKey
                                              , onixProduct.PrimaryAuthor.OnixKeyNames);

                    senderNames = String.Format(Onix3SenderNameFormat
                                                , onixProduct.PrimaryAuthor.OnixKeyNames);
                }

                header = String.Format(Onix3HeaderFormat
                                       , senderIds
                                       , senderNames
                                       , DateTime.Now.ToString("YYYYMMDD")
                                       , headerMsgNote ?? String.Empty);

                contribList = String.Format(Onix3BasicCntbFormat, cntbIds, cntbNames);
            }

            return String.Format(Onix3BasicMessageFormat
                                 , header
                                 , onixProduct.ProductForm
                                 , onixProduct.Title
                                 , contribList
                                 , onixProduct.DescriptiveDetail?.LanguageOfText ?? String.Empty
                                 , onixProduct.DescriptiveDetail?.OnixMainSubjectList[0].MainSubject ?? String.Empty
                                 , onixProduct.PublisherName
                                 , onixProduct.PublishingDetail?.PublicationDate
                                );

        }
    }

}
